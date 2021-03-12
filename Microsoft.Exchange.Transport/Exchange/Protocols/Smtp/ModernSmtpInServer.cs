using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000507 RID: 1287
	internal class ModernSmtpInServer : ISmtpInServer
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000F8206 File Offset: 0x000F6406
		public string Name
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000F820D File Offset: 0x000F640D
		public Version Version
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000F8214 File Offset: 0x000F6414
		public TransportConfigContainer TransportSettings
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000F821B File Offset: 0x000F641B
		public ITransportConfiguration Configuration
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000F8222 File Offset: 0x000F6422
		public Server ServerConfiguration
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000F8229 File Offset: 0x000F6429
		public bool IsBridgehead
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000F8230 File Offset: 0x000F6430
		public ICertificateValidator CertificateValidator
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000F8237 File Offset: 0x000F6437
		public IShadowRedundancyManager ShadowRedundancyManager
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000F823E File Offset: 0x000F643E
		public ICategorizer Categorizer
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000F8245 File Offset: 0x000F6445
		public IInboundProxyDestinationTracker InboundProxyDestinationTracker
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000F824C File Offset: 0x000F644C
		public IInboundProxyDestinationTracker InboundProxyAccountForestTracker
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000F8253 File Offset: 0x000F6453
		public ICertificateCache CertificateCache
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000F825A File Offset: 0x000F645A
		public SmtpProxyPerfCountersWrapper ClientProxyPerfCounters
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000F8261 File Offset: 0x000F6461
		public SmtpProxyPerfCountersWrapper OutboundProxyPerfCounters
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000F8268 File Offset: 0x000F6468
		public OutboundProxyBySourceTracker OutboundProxyBySourceTracker
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000F826F File Offset: 0x000F646F
		public SmtpOutConnectionHandler SmtpOutConnectionHandler
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000F8276 File Offset: 0x000F6476
		public ISmtpInMailItemStorage MailItemStorage
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000F827D File Offset: 0x000F647D
		public IProxyHubSelector ProxyHubSelector
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000F8284 File Offset: 0x000F6484
		public ISmtpReceiveConfiguration ReceiveConfiguration
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000F828B File Offset: 0x000F648B
		public IPConnectionTable InboundTlsIPConnectionTable
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000F8292 File Offset: 0x000F6492
		public IEventNotificationItem EventNotificationItem
		{
			get
			{
				throw ModernSmtpInServer.LegacyStackOnlyException;
			}
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000F8299 File Offset: 0x000F6499
		public void RemoveConnection(long id)
		{
			throw ModernSmtpInServer.LegacyStackOnlyException;
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000F82A0 File Offset: 0x000F64A0
		// (set) Token: 0x06003B5D RID: 15197 RVA: 0x000F82AD File Offset: 0x000F64AD
		public ServiceState TargetRunningState
		{
			get
			{
				return this.serverState.ServiceState;
			}
			set
			{
				this.serverState.ServiceState = value;
			}
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000F82BB File Offset: 0x000F64BB
		public void SetRejectState(bool rejectCommands, bool rejectMailSubmission, bool rejectMailFromInternet, SmtpResponse rejectionResponse)
		{
			this.serverState.SetRejectState(rejectCommands, rejectMailSubmission, rejectMailFromInternet, rejectionResponse);
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000F82CD File Offset: 0x000F64CD
		public bool RejectCommands
		{
			get
			{
				return this.serverState.RejectCommands;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06003B60 RID: 15200 RVA: 0x000F82DA File Offset: 0x000F64DA
		public bool RejectSubmits
		{
			get
			{
				return this.serverState.RejectSubmits;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000F82E7 File Offset: 0x000F64E7
		public bool RejectMailFromInternet
		{
			get
			{
				return this.serverState.RejectMailFromInternet;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000F82F4 File Offset: 0x000F64F4
		public SmtpResponse RejectionSmtpResponse
		{
			get
			{
				return this.serverState.RejectionSmtpResponse;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000F8301 File Offset: 0x000F6501
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x000F830E File Offset: 0x000F650E
		public DateTime CurrentTime
		{
			get
			{
				return this.serverState.CurrentTime;
			}
			set
			{
				this.serverState.CurrentTime = value;
			}
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000F831C File Offset: 0x000F651C
		public void SetThrottleState(TimeSpan perMessageDelay, string diagnosticContext)
		{
			this.serverState.SetThrottleState(perMessageDelay, diagnosticContext);
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000F832B File Offset: 0x000F652B
		public TimeSpan ThrottleDelay
		{
			get
			{
				return this.serverState.ThrottleDelay;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000F8338 File Offset: 0x000F6538
		public string ThrottleDelayContext
		{
			get
			{
				return this.serverState.ThrottleDelayContext;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000F8345 File Offset: 0x000F6545
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000F834D File Offset: 0x000F654D
		public bool Ipv6ReceiveConnectionThrottlingEnabled { get; private set; }

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000F8356 File Offset: 0x000F6556
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000F835E File Offset: 0x000F655E
		public bool ReceiveTlsThrottlingEnabled { get; private set; }

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000F8367 File Offset: 0x000F6567
		public string CurrentState
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000F8370 File Offset: 0x000F6570
		public void SetRunTimeDependencies(IAgentRuntime agentRuntime, IMailRouter mailRouter, IProxyHubSelector proxyHubSelector, IEnhancedDns enhancedDns, ICategorizer categorizer, ICertificateCache certificateCache, ICertificateValidator certificateValidator, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, ISmtpInMailItemStorage mailItemStorage, SmtpOutConnectionHandler smtpOutConnectionHandler, IQueueQuotaComponent queueQuotaComponent)
		{
			ArgumentValidator.ThrowIfNull("agentRuntime", agentRuntime);
			ArgumentValidator.ThrowIfNull("categorizer", categorizer);
			ArgumentValidator.ThrowIfNull("certificateCache", certificateCache);
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("memberOfResolver", memberOfResolver);
			ArgumentValidator.ThrowIfNull("messageThrottlingManager", messageThrottlingManager);
			ArgumentValidator.ThrowIfNull("mailItemStorage", mailItemStorage);
			this.serverState.SetRunTimeDependencies(agentRuntime, categorizer, certificateCache, certificateValidator, memberOfResolver, messageThrottlingManager, shadowRedundancyManager, mailItemStorage, queueQuotaComponent);
			this.receiveConnectorManager = this.CreateReceiveConnectorManager(this.serverState.SmtpConfiguration);
			this.OnTransportServerConfigurationChanged(this.configuration.LocalServer);
			this.OnReceiveConnectorsChanged(this.configuration.LocalReceiveConnectors);
			this.runtimeDependenciesSet = true;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000F842C File Offset: 0x000F662C
		public void SetLoadTimeDependencies(IProtocolLog protocolLogToUse, ITransportAppConfig transportAppConfigToUse, ITransportConfiguration transportConfigurationToUse)
		{
			ArgumentValidator.ThrowIfNull("protocolLogToUse", protocolLogToUse);
			ArgumentValidator.ThrowIfNull("transportAppConfigToUse", transportAppConfigToUse);
			ArgumentValidator.ThrowIfNull("transportConfigurationToUse", transportConfigurationToUse);
			this.Ipv6ReceiveConnectionThrottlingEnabled = transportAppConfigToUse.SmtpReceiveConfiguration.Ipv6ReceiveConnectionThrottlingEnabled;
			this.ReceiveTlsThrottlingEnabled = transportAppConfigToUse.SmtpReceiveConfiguration.ReceiveTlsThrottlingEnabled;
			this.configuration = transportConfigurationToUse;
			this.serverState.SetLoadTimeDependencies(protocolLogToUse, transportAppConfigToUse, transportConfigurationToUse);
			this.loadtimeDependenciesSet = true;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000F8498 File Offset: 0x000F6698
		public void Load()
		{
			this.ThrowIfLoadTimeDependenciesNotSet();
			this.ConfigureProtocolLog(this.serverState.SmtpConfiguration.TransportConfiguration.LocalServer);
			this.configuration.LocalServerChanged += this.OnTransportServerConfigurationChanged;
			this.configuration.LocalReceiveConnectorsChanged += this.OnReceiveConnectorsChanged;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000F84F4 File Offset: 0x000F66F4
		public void Unload()
		{
			this.ThrowIfLoadTimeDependenciesNotSet();
			this.configuration.LocalServerChanged -= this.OnTransportServerConfigurationChanged;
			this.configuration.LocalReceiveConnectorsChanged -= this.OnReceiveConnectorsChanged;
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000F852C File Offset: 0x000F672C
		public void Initialize(TcpListener.HandleFailure failureDelegate = null, TcpListener.HandleConnection connectionHandler = null)
		{
			this.ThrowIfLoadTimeDependenciesNotSet();
			this.ThrowIfRunTimeDependenciesNotSet();
			this.OnTransportServerConfigurationChanged(this.configuration.LocalServer);
			this.tcpListener = this.CreateTcpListener(failureDelegate, connectionHandler);
			this.OnReceiveConnectorsChanged(this.configuration.LocalReceiveConnectors);
			this.tcpListener.StartListening(true);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000F8584 File Offset: 0x000F6784
		public void Shutdown()
		{
			this.ThrowIfLoadTimeDependenciesNotSet();
			this.ThrowIfRunTimeDependenciesNotSet();
			this.ThrowIfShuttingDown();
			if (this.tcpListener != null)
			{
				this.tcpListener.ProcessStopping = true;
				this.tcpListener.StopListening();
				this.tcpListener.Shutdown();
				this.tcpListener = null;
			}
			this.cancellationTokenSource.Cancel();
			this.WaitForAllStateMachinesToTerminate(ModernSmtpInServer.ShutdownTimeout);
			this.serverState.ProtocolLog.Close();
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000F85FA File Offset: 0x000F67FA
		public void NonGracefullyCloseTcpListener()
		{
			if (this.tcpListener != null)
			{
				this.tcpListener.StopListening();
				this.tcpListener = null;
			}
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000F8616 File Offset: 0x000F6816
		public INetworkConnection CreateNetworkConnection(Socket socket, int receiveBufferSize)
		{
			ArgumentValidator.ThrowIfNull("socket", socket);
			return new CancellableNetworkConnection(socket, this.cancellationTokenSource.Token, receiveBufferSize);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000F8694 File Offset: 0x000F6894
		public bool HandleConnection(INetworkConnection connection)
		{
			ArgumentValidator.ThrowIfNull("connection", connection);
			this.ThrowIfLoadTimeDependenciesNotSet();
			this.ThrowIfRunTimeDependenciesNotSet();
			if (this.ShouldRejectConnectionOnFrontEndRole(connection))
			{
				return false;
			}
			SmtpReceiveConnectorStub smtpReceiveConnectorStub;
			if (!this.receiveConnectorManager.TryLookupIncomingConnection(connection.LocalEndPoint, connection.RemoteEndPoint, out smtpReceiveConnectorStub))
			{
				return false;
			}
			connection.MaxLineLength = 4000;
			connection.Timeout = (int)smtpReceiveConnectorStub.Connector.ConnectionInactivityTimeout.TotalSeconds;
			MailboxTransportSmtpInStateMachine mailboxTransportSmtpInStateMachine = new MailboxTransportSmtpInStateMachine(new SmtpInSessionState(this.serverState, connection, smtpReceiveConnectorStub));
			Task task = mailboxTransportSmtpInStateMachine.ExecuteAsync(this.cancellationTokenSource.Token);
			if (this.currentSessions.TryAdd(connection.ConnectionId, task))
			{
				task.ContinueWith(delegate(Task completedTask)
				{
					Task task2;
					if (!this.currentSessions.TryRemove(connection.ConnectionId, out task2))
					{
						this.serverState.Tracer.TraceDebug(connection.ConnectionId, "this.currentSessions.TryRemove() returned false");
					}
				});
			}
			else
			{
				this.serverState.Tracer.TraceDebug(connection.ConnectionId, "this.currentSessions.TryAdd() returned false");
			}
			return true;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000F87C5 File Offset: 0x000F69C5
		public void AddDiagnosticInfo(DiagnosableParameters parameters, XElement element)
		{
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000F87C7 File Offset: 0x000F69C7
		protected virtual ReceiveConnectorManager CreateReceiveConnectorManager(ISmtpReceiveConfiguration config)
		{
			return new ReceiveConnectorManager(config);
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000F87D0 File Offset: 0x000F69D0
		protected virtual ITcpListener CreateTcpListener(TcpListener.HandleFailure failureDelegate, TcpListener.HandleConnection connectionHandler)
		{
			return new TcpListener(failureDelegate, connectionHandler, null, this.serverState.Tracer, this.serverState.EventLog, 1200, this.serverState.SmtpConfiguration.TransportConfiguration.ExclusiveAddressUse, this.serverState.SmtpConfiguration.TransportConfiguration.DisableHandleInheritance);
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000F882A File Offset: 0x000F6A2A
		protected void WaitForAllStateMachinesToTerminate(TimeSpan timeout)
		{
			if (!Task.WaitAll(this.currentSessions.Values.ToArray<Task>(), timeout))
			{
				this.LogTimedOutWaitingForStateMachinesToTerminate(timeout);
			}
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000F884C File Offset: 0x000F6A4C
		protected virtual void LogTimedOutWaitingForStateMachinesToTerminate(TimeSpan timeout)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Timed out waiting for all state machines to terminate (duration {0}", timeout);
			stringBuilder.AppendFormat("Number of sessions remaining: {0}", this.currentSessions.Count);
			Exception ex = new InvalidOperationException(stringBuilder.ToString());
			bool flag;
			ExWatson.SendThrottledGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, ex.GetType().Name, ex.StackTrace, ex.GetHashCode().ToString(CultureInfo.InvariantCulture), ex.TargetSite.Name, "details", TimeSpan.FromHours(1.0), out flag);
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000F890C File Offset: 0x000F6B0C
		private void OnTransportServerConfigurationChanged(TransportServerConfiguration config)
		{
			this.ConfigureProtocolLog(config.TransportServer);
			if (this.tcpListener != null)
			{
				this.tcpListener.MaxConnectionRate = config.TransportServer.MaxConnectionRatePerMinute;
			}
			if (this.receiveConnectorManager != null)
			{
				this.receiveConnectorManager.ApplyLocalServerConfiguration(config.TransportServer);
			}
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000F895C File Offset: 0x000F6B5C
		private void OnReceiveConnectorsChanged(ReceiveConnectorConfiguration connectorConfig)
		{
			this.receiveConnectorManager.ApplyReceiveConnectors(connectorConfig.Connectors, this.serverState.SmtpConfiguration.TransportConfiguration.LocalServer);
			if (this.tcpListener != null)
			{
				this.tcpListener.SetBindings(this.receiveConnectorManager.Bindings.ToArray(), true);
			}
			this.serverState.EventLog.LogEvent(TransportEventLogConstants.Tuple_ConfiguredConnectors, null, null);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000F89CB File Offset: 0x000F6BCB
		private bool ShouldRejectConnectionOnFrontEndRole(INetworkConnection connection)
		{
			return this.serverState.SmtpConfiguration.TransportConfiguration.ProcessTransportRole == ProcessTransportRole.FrontEnd && this.TargetRunningState == ServiceState.Inactive && !this.serverState.IsLocalAddress(connection.RemoteEndPoint.Address);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000F8A08 File Offset: 0x000F6C08
		private void ConfigureProtocolLog(Server server)
		{
			this.serverState.ProtocolLog.Configure(server.ReceiveProtocolLogPath, server.ReceiveProtocolLogMaxAge, server.ReceiveProtocolLogMaxDirectorySize, server.ReceiveProtocolLogMaxFileSize, this.serverState.SmtpConfiguration.DiagnosticsConfiguration.SmtpRecvLogBufferSize, this.serverState.SmtpConfiguration.DiagnosticsConfiguration.SmtpRecvLogFlushInterval, this.serverState.SmtpConfiguration.DiagnosticsConfiguration.SmtpRecvLogAsyncInterval);
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000F8A81 File Offset: 0x000F6C81
		private void ThrowIfLoadTimeDependenciesNotSet()
		{
			if (!this.loadtimeDependenciesSet)
			{
				throw new InvalidOperationException("SetLoadTimeDependencies() must be invoked before other methods are usable");
			}
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000F8A96 File Offset: 0x000F6C96
		private void ThrowIfRunTimeDependenciesNotSet()
		{
			if (!this.runtimeDependenciesSet)
			{
				throw new InvalidOperationException("SetRunTimeDependencies() must be invoked before other methods are usable");
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000F8AAC File Offset: 0x000F6CAC
		private void ThrowIfShuttingDown()
		{
			if (this.cancellationTokenSource.Token.IsCancellationRequested)
			{
				throw new InvalidOperationException("Shutdown() can only be invoked once");
			}
		}

		// Token: 0x04001DE7 RID: 7655
		private static readonly InvalidOperationException LegacyStackOnlyException = new InvalidOperationException("This method is not used by the new SMTP stack");

		// Token: 0x04001DE8 RID: 7656
		private static readonly TimeSpan ShutdownTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x04001DE9 RID: 7657
		private readonly SmtpInServerState serverState = new SmtpInServerState();

		// Token: 0x04001DEA RID: 7658
		private readonly ConcurrentDictionary<long, Task> currentSessions = new ConcurrentDictionary<long, Task>();

		// Token: 0x04001DEB RID: 7659
		private bool runtimeDependenciesSet;

		// Token: 0x04001DEC RID: 7660
		private bool loadtimeDependenciesSet;

		// Token: 0x04001DED RID: 7661
		private ITcpListener tcpListener;

		// Token: 0x04001DEE RID: 7662
		private ReceiveConnectorManager receiveConnectorManager;

		// Token: 0x04001DEF RID: 7663
		private ITransportConfiguration configuration;

		// Token: 0x04001DF0 RID: 7664
		private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	}
}
