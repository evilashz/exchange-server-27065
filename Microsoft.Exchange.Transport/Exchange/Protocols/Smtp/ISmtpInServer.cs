using System;
using System.Net.Sockets;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000434 RID: 1076
	internal interface ISmtpInServer
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600316B RID: 12651
		string Name { get; }

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x0600316C RID: 12652
		Version Version { get; }

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600316D RID: 12653
		// (set) Token: 0x0600316E RID: 12654
		ServiceState TargetRunningState { get; set; }

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600316F RID: 12655
		TransportConfigContainer TransportSettings { get; }

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003170 RID: 12656
		ITransportConfiguration Configuration { get; }

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003171 RID: 12657
		Server ServerConfiguration { get; }

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003172 RID: 12658
		bool IsBridgehead { get; }

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003173 RID: 12659
		ICertificateValidator CertificateValidator { get; }

		// Token: 0x06003174 RID: 12660
		void SetRejectState(bool rejectCommands, bool rejectMailSubmission, bool rejectMailFromInternet, SmtpResponse rejectionResponse);

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003175 RID: 12661
		bool RejectCommands { get; }

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003176 RID: 12662
		bool RejectSubmits { get; }

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003177 RID: 12663
		bool RejectMailFromInternet { get; }

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003178 RID: 12664
		SmtpResponse RejectionSmtpResponse { get; }

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003179 RID: 12665
		// (set) Token: 0x0600317A RID: 12666
		DateTime CurrentTime { get; set; }

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600317B RID: 12667
		IShadowRedundancyManager ShadowRedundancyManager { get; }

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600317C RID: 12668
		ICategorizer Categorizer { get; }

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600317D RID: 12669
		IInboundProxyDestinationTracker InboundProxyDestinationTracker { get; }

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600317E RID: 12670
		IInboundProxyDestinationTracker InboundProxyAccountForestTracker { get; }

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x0600317F RID: 12671
		ICertificateCache CertificateCache { get; }

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003180 RID: 12672
		SmtpProxyPerfCountersWrapper ClientProxyPerfCounters { get; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003181 RID: 12673
		SmtpProxyPerfCountersWrapper OutboundProxyPerfCounters { get; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003182 RID: 12674
		OutboundProxyBySourceTracker OutboundProxyBySourceTracker { get; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003183 RID: 12675
		SmtpOutConnectionHandler SmtpOutConnectionHandler { get; }

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003184 RID: 12676
		ISmtpInMailItemStorage MailItemStorage { get; }

		// Token: 0x06003185 RID: 12677
		void SetThrottleState(TimeSpan perMessageDelay, string diagnosticContext);

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003186 RID: 12678
		TimeSpan ThrottleDelay { get; }

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003187 RID: 12679
		string ThrottleDelayContext { get; }

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003188 RID: 12680
		IProxyHubSelector ProxyHubSelector { get; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003189 RID: 12681
		ISmtpReceiveConfiguration ReceiveConfiguration { get; }

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x0600318A RID: 12682
		IPConnectionTable InboundTlsIPConnectionTable { get; }

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x0600318B RID: 12683
		bool Ipv6ReceiveConnectionThrottlingEnabled { get; }

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x0600318C RID: 12684
		bool ReceiveTlsThrottlingEnabled { get; }

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x0600318D RID: 12685
		IEventNotificationItem EventNotificationItem { get; }

		// Token: 0x0600318E RID: 12686
		void RemoveConnection(long id);

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x0600318F RID: 12687
		string CurrentState { get; }

		// Token: 0x06003190 RID: 12688
		void SetRunTimeDependencies(IAgentRuntime agentRuntime, IMailRouter mailRouter, IProxyHubSelector proxyHubSelector, IEnhancedDns enhancedDns, ICategorizer categorizer, ICertificateCache certificateCache, ICertificateValidator certificateValidator, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, ISmtpInMailItemStorage mailItemStorage, SmtpOutConnectionHandler smtpOutConnectionHandler, IQueueQuotaComponent queueQuotaComponent);

		// Token: 0x06003191 RID: 12689
		void SetLoadTimeDependencies(IProtocolLog protocolLog, ITransportAppConfig transportAppConfig, ITransportConfiguration configuration);

		// Token: 0x06003192 RID: 12690
		void Load();

		// Token: 0x06003193 RID: 12691
		void Unload();

		// Token: 0x06003194 RID: 12692
		void Initialize(TcpListener.HandleFailure failureDelegate = null, TcpListener.HandleConnection connectionHandler = null);

		// Token: 0x06003195 RID: 12693
		void Shutdown();

		// Token: 0x06003196 RID: 12694
		void NonGracefullyCloseTcpListener();

		// Token: 0x06003197 RID: 12695
		INetworkConnection CreateNetworkConnection(Socket socket, int receiveBufferSize);

		// Token: 0x06003198 RID: 12696
		bool HandleConnection(INetworkConnection connection);

		// Token: 0x06003199 RID: 12697
		void AddDiagnosticInfo(DiagnosableParameters parameters, XElement element);
	}
}
