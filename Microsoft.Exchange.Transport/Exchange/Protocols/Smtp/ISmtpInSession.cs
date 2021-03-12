using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000435 RID: 1077
	internal interface ISmtpInSession : ISmtpSession
	{
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x0600319A RID: 12698
		X509Certificate2 AdvertisedTlsCertificate { get; }

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x0600319B RID: 12699
		INetworkConnection NetworkConnection { get; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x0600319C RID: 12700
		long ConnectionId { get; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x0600319D RID: 12701
		IPAddress ProxiedClientAddress { get; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x0600319E RID: 12702
		string ProxyHopHelloDomain { get; }

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x0600319F RID: 12703
		IPAddress ProxyHopAddress { get; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060031A0 RID: 12704
		// (set) Token: 0x060031A1 RID: 12705
		InboundClientProxyStates InboundClientProxyState { get; set; }

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060031A2 RID: 12706
		X509Certificate2 InternalTransportCertificate { get; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060031A3 RID: 12707
		bool IsAnonymousClientProxiedSession { get; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060031A4 RID: 12708
		// (set) Token: 0x060031A5 RID: 12709
		bool StartClientProxySession { get; set; }

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060031A6 RID: 12710
		// (set) Token: 0x060031A7 RID: 12711
		string ProxyUserName { get; set; }

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060031A8 RID: 12712
		// (set) Token: 0x060031A9 RID: 12713
		SecureString ProxyPassword { get; set; }

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060031AA RID: 12714
		// (set) Token: 0x060031AB RID: 12715
		bool ClientProxyFailedDueToIncompatibleBackend { get; set; }

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060031AC RID: 12716
		Permission Permissions { get; }

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060031AD RID: 12717
		// (set) Token: 0x060031AE RID: 12718
		TransportMiniRecipient AuthUserRecipient { get; set; }

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060031AF RID: 12719
		AgentLatencyTracker AgentLatencyTracker { get; }

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060031B0 RID: 12720
		SmtpSession SessionSource { get; }

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060031B1 RID: 12721
		// (set) Token: 0x060031B2 RID: 12722
		TransportMailItemWrapper TransportMailItemWrapper { get; set; }

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060031B3 RID: 12723
		// (set) Token: 0x060031B4 RID: 12724
		bool SendAsRequiredADLookup { get; set; }

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060031B5 RID: 12725
		// (set) Token: 0x060031B6 RID: 12726
		bool SeenHelo { get; set; }

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060031B7 RID: 12727
		// (set) Token: 0x060031B8 RID: 12728
		bool SeenEhlo { get; set; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060031B9 RID: 12729
		// (set) Token: 0x060031BA RID: 12730
		bool SeenRcpt2 { get; set; }

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060031BB RID: 12731
		// (set) Token: 0x060031BC RID: 12732
		string HelloSmtpDomain { get; set; }

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060031BD RID: 12733
		bool IsBdatOngoing { get; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060031BE RID: 12734
		bool IsXexch50Received { get; }

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060031BF RID: 12735
		bool IsTls { get; }

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060031C0 RID: 12736
		// (set) Token: 0x060031C1 RID: 12737
		MultilevelAuthMechanism AuthMethod { get; set; }

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060031C2 RID: 12738
		AuthenticationSource AuthenticationSourceForAgents { get; }

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060031C3 RID: 12739
		TransportMailItem TransportMailItem { get; }

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060031C4 RID: 12740
		ISmtpAgentSession AgentSession { get; }

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060031C5 RID: 12741
		IIsMemberOfResolver<RoutingAddress> IsMemberOfResolver { get; }

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x060031C6 RID: 12742
		ISmtpInServer SmtpInServer { get; }

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x060031C7 RID: 12743
		IPEndPoint ClientEndPoint { get; }

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060031C8 RID: 12744
		bool ShutdownConnectionCalled { get; }

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060031C9 RID: 12745
		IProtocolLogSession LogSession { get; }

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060031CA RID: 12746
		ulong SessionId { get; }

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060031CB RID: 12747
		ClientData ClientIPData { get; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060031CC RID: 12748
		IPEndPoint RemoteEndPoint { get; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060031CD RID: 12749
		IPEndPoint LocalEndPoint { get; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060031CE RID: 12750
		IEhloOptions AdvertisedEhloOptions { get; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060031CF RID: 12751
		// (set) Token: 0x060031D0 RID: 12752
		string SenderShadowContext { get; set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060031D1 RID: 12753
		bool IsShadowedBySender { get; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060031D2 RID: 12754
		// (set) Token: 0x060031D3 RID: 12755
		string PeerSessionPrimaryServer { get; set; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060031D4 RID: 12756
		bool IsPeerShadowSession { get; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060031D5 RID: 12757
		bool ShouldProxyClientSession { get; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060031D6 RID: 12758
		SmtpReceiveCapabilities Capabilities { get; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060031D7 RID: 12759
		SmtpReceiveCapabilities? TlsDomainCapabilities { get; }

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060031D8 RID: 12760
		bool AcceptLongAddresses { get; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060031D9 RID: 12761
		// (set) Token: 0x060031DA RID: 12762
		XProxyToSmtpCommandParser XProxyToParser { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060031DB RID: 12763
		ITransportAppConfig TransportAppConfig { get; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060031DC RID: 12764
		InboundRecipientCorrelator RecipientCorrelator { get; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060031DD RID: 12765
		bool DiscardingMessage { get; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060031DE RID: 12766
		ChainValidityStatus TlsRemoteCertificateChainValidationStatus { get; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060031DF RID: 12767
		X509Certificate2 TlsRemoteCertificate { get; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060031E0 RID: 12768
		SecureState SecureState { get; }

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060031E1 RID: 12769
		Breadcrumbs<SmtpInSessionBreadcrumbs> Breadcrumbs { get; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060031E2 RID: 12770
		// (set) Token: 0x060031E3 RID: 12771
		MailCommandMessageContextParameters MailCommandMessageContextInformation { get; set; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060031E4 RID: 12772
		// (set) Token: 0x060031E5 RID: 12773
		string RemoteIdentityName { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060031E6 RID: 12774
		// (set) Token: 0x060031E7 RID: 12775
		SecurityIdentifier RemoteIdentity { get; set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060031E8 RID: 12776
		// (set) Token: 0x060031E9 RID: 12777
		WindowsIdentity RemoteWindowsIdentity { get; set; }

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060031EA RID: 12778
		string CurrentMessageTemporaryId { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060031EB RID: 12779
		ExEventLog EventLogger { get; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060031EC RID: 12780
		// (set) Token: 0x060031ED RID: 12781
		bool DisableStartTls { get; set; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060031EE RID: 12782
		// (set) Token: 0x060031EF RID: 12783
		bool ForceRequestClientTlsCertificate { get; set; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060031F0 RID: 12784
		SmtpProxyPerfCountersWrapper SmtpProxyPerfCounters { get; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060031F1 RID: 12785
		ChannelBindingToken ChannelBindingToken { get; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060031F2 RID: 12786
		ExtendedProtectionConfig ExtendedProtectionConfig { get; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060031F3 RID: 12787
		IMessageThrottlingManager MessageThrottlingManager { get; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060031F4 RID: 12788
		IQueueQuotaComponent QueueQuotaComponent { get; }

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060031F5 RID: 12789
		ReceiveConnector Connector { get; }

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060031F6 RID: 12790
		SmtpReceiveConnectorStub ConnectorStub { get; }

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060031F7 RID: 12791
		string AdvertisedDomain { get; }

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060031F8 RID: 12792
		// (set) Token: 0x060031F9 RID: 12793
		Permission SessionPermissions { get; set; }

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060031FA RID: 12794
		// (set) Token: 0x060031FB RID: 12795
		int LogonFailures { get; set; }

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060031FC RID: 12796
		int MaxLogonFailures { get; }

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060031FD RID: 12797
		// (set) Token: 0x060031FE RID: 12798
		bool TarpitRset { get; set; }

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060031FF RID: 12799
		// (set) Token: 0x06003200 RID: 12800
		SmtpInBdatState BdatState { get; set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003201 RID: 12801
		MimeDocument MimeDocument { get; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003202 RID: 12802
		Stream MessageWriteStream { get; }

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003203 RID: 12803
		bool StartTlsSupported { get; }

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003204 RID: 12804
		bool AnonymousTlsSupported { get; }

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003205 RID: 12805
		ISmtpReceivePerfCounters SmtpReceivePerformanceCounters { get; }

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003206 RID: 12806
		IInboundProxyDestinationPerfCounters InboundProxyDestinationPerfCounters { get; }

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003207 RID: 12807
		IInboundProxyDestinationPerfCounters InboundProxyAccountForestPerfCounters { get; }

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003208 RID: 12808
		// (set) Token: 0x06003209 RID: 12809
		InboundExch50 InboundExch50 { get; set; }

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600320A RID: 12810
		// (set) Token: 0x0600320B RID: 12811
		int TooManyRecipientsResponseCount { get; set; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600320C RID: 12812
		byte[] TlsEapKey { get; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600320D RID: 12813
		int TlsCipherKeySize { get; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600320E RID: 12814
		uint XProxyFromSeqNum { get; }

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600320F RID: 12815
		IShadowRedundancyManager ShadowRedundancyManagerObject { get; }

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003210 RID: 12816
		// (set) Token: 0x06003211 RID: 12817
		IShadowSession ShadowSession { get; set; }

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003212 RID: 12818
		bool SupportIntegratedAuth { get; }

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003213 RID: 12819
		Permission ProxiedClientPermissions { get; }

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003214 RID: 12820
		// (set) Token: 0x06003215 RID: 12821
		IMExSession MexSession { get; set; }

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003216 RID: 12822
		IAuthzAuthorization AuthzAuthorization { get; }

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003217 RID: 12823
		ISmtpMessageContextBlob MessageContextBlob { get; }

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003218 RID: 12824
		bool IsDataRedactionNecessary { get; }

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003219 RID: 12825
		ITracer Tracer { get; }

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x0600321A RID: 12826
		Permission AnonymousPermissions { get; }

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x0600321B RID: 12827
		Permission PartnerPermissions { get; }

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600321C RID: 12828
		// (set) Token: 0x0600321D RID: 12829
		bool SmtpUtf8Supported { get; set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600321E RID: 12830
		DateTime SessionStartTime { get; }

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600321F RID: 12831
		int NumberOfMessagesReceived { get; }

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003220 RID: 12832
		// (set) Token: 0x06003221 RID: 12833
		string DestinationTrackerLastNextHopFqdn { get; set; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003222 RID: 12834
		// (set) Token: 0x06003223 RID: 12835
		string DestinationTrackerLastExoAccountForest { get; set; }

		// Token: 0x06003224 RID: 12836
		void DropBreadcrumb(SmtpInSessionBreadcrumbs breadcrumb);

		// Token: 0x06003225 RID: 12837
		void LogInformation(ProtocolLoggingLevel loggingLevel, string information, byte[] data);

		// Token: 0x06003226 RID: 12838
		void Start();

		// Token: 0x06003227 RID: 12839
		void SetupSessionToProxyTarget(SmtpSendConnectorConfig outboundProxyConnector, IEnumerable<INextHopServer> outboundProxyDestinationsParam, TlsSendConfiguration outboundProxyTlsSendConfigurationParam, RiskLevel outboundProxyRiskLevelParam, int outboundProxyOutboundIPPoolParam, string outboundProxyNextHopDomainParam, string outboundProxySessionIdParam);

		// Token: 0x06003228 RID: 12840
		void SetupExpectedBlobs(MailCommandMessageContextParameters messageContextParameters);

		// Token: 0x06003229 RID: 12841
		void ResetExpectedBlobs();

		// Token: 0x0600322A RID: 12842
		bool ShouldRejectMailItem(RoutingAddress fromAddress, bool checkRecipientCount, out SmtpResponse failureSmtpResponse);

		// Token: 0x0600322B RID: 12843
		SmtpResponse TrackAndEnqueueMailItem();

		// Token: 0x0600322C RID: 12844
		void TrackAndEnqueuePeerShadowMailItem();

		// Token: 0x0600322D RID: 12845
		void ReleaseMailItem();

		// Token: 0x0600322E RID: 12846
		void UpdateSessionWithProxyInformation(IPAddress clientIp, int clientPort, string clientHelloDomain, bool isAuthenticatedProxy, SecurityIdentifier securityId, string clientIdentityName, WindowsIdentity identity, TransportMiniRecipient recipient, int? capabilitiesInt);

		// Token: 0x0600322F RID: 12847
		void UpdateSessionWithProxyFromInformation(IPAddress clientIp, int clientPort, string clientHelloDomain, uint xProxyFromSequenceNum, uint? permissionsInt, AuthenticationSource? authSource);

		// Token: 0x06003230 RID: 12848
		void Shutdown();

		// Token: 0x06003231 RID: 12849
		void ShutdownConnection();

		// Token: 0x06003232 RID: 12850
		void Disconnect(DisconnectReason disconnectReasonParam);

		// Token: 0x06003233 RID: 12851
		void HandleBlindProxySetupFailure(SmtpResponse response, bool clientProxy);

		// Token: 0x06003234 RID: 12852
		void ResetSessionAuthentication();

		// Token: 0x06003235 RID: 12853
		void HandleBlindProxySetupSuccess(SmtpResponse successfulResponse, NetworkConnection networkConnection, ulong sendSessionId, IProtocolLogSession sendLogSession, bool isClientProxy);

		// Token: 0x06003236 RID: 12854
		byte[] GetCertificatePublicKey();

		// Token: 0x06003237 RID: 12855
		bool IsTrustedIP(IPAddress address);

		// Token: 0x06003238 RID: 12856
		bool DetermineTlsDomainCapabilities();

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003239 RID: 12857
		Permission MailItemPermissionsGranted { get; }

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x0600323A RID: 12858
		Permission MailItemPermissionsDenied { get; }

		// Token: 0x0600323B RID: 12859
		void GrantMailItemPermissions(Permission permissions);

		// Token: 0x0600323C RID: 12860
		void DenyMailItemPermissions(Permission permissions);

		// Token: 0x0600323D RID: 12861
		void ResetMailItemPermissions();

		// Token: 0x0600323E RID: 12862
		void RemoveClientIpConnection();

		// Token: 0x0600323F RID: 12863
		void UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory category);

		// Token: 0x06003240 RID: 12864
		void IncrementSmtpAvailabilityPerfCounterForMessageLoopsInLastHour(long incrementValue);

		// Token: 0x06003241 RID: 12865
		bool CreateTransportMailItem(OrganizationId mailCommandInternalOrganizationId, Guid mailCommandExternalOrganizationId, MailDirectionality mailCommandDirectionality, string exoAccountForest, string exoTenantContainer, out SmtpResponse smtpResponse);

		// Token: 0x06003242 RID: 12866
		void DeleteTransportMailItem();

		// Token: 0x06003243 RID: 12867
		void AbortMailTransaction();

		// Token: 0x06003244 RID: 12868
		void UpdateSmtpReceivePerfCountersForMessageReceived(int recipients, long messageBytes);

		// Token: 0x06003245 RID: 12869
		void UpdateInboundProxyDestinationPerfCountersForMessageReceived(int recipients, long messageBytes);

		// Token: 0x06003246 RID: 12870
		Stream OpenMessageWriteStream(bool expectBinaryContent);

		// Token: 0x06003247 RID: 12871
		void CloseMessageWriteStream();

		// Token: 0x06003248 RID: 12872
		void PutBackReceivedBytes(int bytesUnconsumed);

		// Token: 0x06003249 RID: 12873
		void RawDataReceivedCompleted();

		// Token: 0x0600324A RID: 12874
		void SetRawModeAfterCommandCompleted(RawDataHandler rawDataHandler);

		// Token: 0x0600324B RID: 12875
		void StartTls(SecureState secureState);

		// Token: 0x0600324C RID: 12876
		IAsyncResult RaiseOnRejectEvent(byte[] command, EventArgs originalEventArgs, SmtpResponse smtpResponse, AsyncCallback callback);

		// Token: 0x0600324D RID: 12877
		byte[] GetTlsEapKey();

		// Token: 0x0600324E RID: 12878
		void SetSessionPermissions(IntPtr userToken);

		// Token: 0x0600324F RID: 12879
		void AddSessionPermissions(SmtpReceiveCapabilities capabilities);

		// Token: 0x06003250 RID: 12880
		void SetupPoisonContext();
	}
}
