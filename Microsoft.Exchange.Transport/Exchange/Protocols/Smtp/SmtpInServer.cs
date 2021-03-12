using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
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
	// Token: 0x020004BF RID: 1215
	internal class SmtpInServer : ISmtpInServer
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x000E0C1C File Offset: 0x000DEE1C
		public SmtpInServer()
		{
			this.currentTime = DateTime.UtcNow;
			this.configUpdateLock = new ReaderWriterLockSlim();
			this.ThrottleDelay = TimeSpan.Zero;
			this.eventLogger = new ExEventLog(ExTraceGlobals.SmtpReceiveTracer.Category, TransportEventLog.GetEventSource());
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000E0CB7 File Offset: 0x000DEEB7
		public IInboundProxyDestinationTracker InboundProxyDestinationTracker
		{
			get
			{
				return this.inboundProxyDestinationTracker;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000E0CBF File Offset: 0x000DEEBF
		public IInboundProxyDestinationTracker InboundProxyAccountForestTracker
		{
			get
			{
				return this.inboundProxyAccountForestTracker;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000E0CC7 File Offset: 0x000DEEC7
		public ICategorizer Categorizer
		{
			get
			{
				return this.categorizer;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000E0CCF File Offset: 0x000DEECF
		public ISmtpInMailItemStorage MailItemStorage
		{
			get
			{
				return this.mailItemStorage;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000E0CD7 File Offset: 0x000DEED7
		public ICertificateCache CertificateCache
		{
			get
			{
				return this.certificateCache;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000E0CDF File Offset: 0x000DEEDF
		public ICertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000E0CE7 File Offset: 0x000DEEE7
		public void SetRejectState(bool rejectCommands, bool rejectMailSubmission, bool rejectMailFromInternet, SmtpResponse rejectionResponse)
		{
			this.RejectCommands = rejectCommands;
			this.RejectSubmits = rejectMailSubmission;
			this.RejectMailFromInternet = rejectMailFromInternet;
			this.RejectionSmtpResponse = rejectionResponse;
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000E0D06 File Offset: 0x000DEF06
		// (set) Token: 0x060036C2 RID: 14018 RVA: 0x000E0D0E File Offset: 0x000DEF0E
		public bool RejectCommands { get; private set; }

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000E0D17 File Offset: 0x000DEF17
		// (set) Token: 0x060036C4 RID: 14020 RVA: 0x000E0D1F File Offset: 0x000DEF1F
		public bool RejectSubmits { get; private set; }

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000E0D28 File Offset: 0x000DEF28
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x000E0D30 File Offset: 0x000DEF30
		public bool RejectMailFromInternet { get; private set; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000E0D39 File Offset: 0x000DEF39
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000E0D41 File Offset: 0x000DEF41
		public SmtpResponse RejectionSmtpResponse { get; private set; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000E0D4A File Offset: 0x000DEF4A
		public IShadowRedundancyManager ShadowRedundancyManager
		{
			get
			{
				return this.shadowRedundancyManager;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000E0D52 File Offset: 0x000DEF52
		public string Name
		{
			get
			{
				return SmtpReceiveServer.ServerName;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000E0D59 File Offset: 0x000DEF59
		public Version Version
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000E0D61 File Offset: 0x000DEF61
		// (set) Token: 0x060036CD RID: 14029 RVA: 0x000E0D69 File Offset: 0x000DEF69
		public ServiceState TargetRunningState
		{
			get
			{
				return this.targetRunningState;
			}
			set
			{
				this.targetRunningState = value;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x000E0D72 File Offset: 0x000DEF72
		// (set) Token: 0x060036CF RID: 14031 RVA: 0x000E0D7A File Offset: 0x000DEF7A
		public DateTime CurrentTime
		{
			get
			{
				return this.currentTime;
			}
			set
			{
				this.currentTime = value;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000E0D83 File Offset: 0x000DEF83
		public ITransportConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000E0D8B File Offset: 0x000DEF8B
		public bool IsBridgehead
		{
			get
			{
				return this.configuration.LocalServer.IsBridgehead;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000E0D9D File Offset: 0x000DEF9D
		public TransportConfigContainer TransportSettings
		{
			get
			{
				return this.configuration.TransportSettings.TransportSettings;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000E0DAF File Offset: 0x000DEFAF
		public Server ServerConfiguration
		{
			get
			{
				return this.configuration.LocalServer.TransportServer;
			}
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000E0DC1 File Offset: 0x000DEFC1
		public void SetThrottleState(TimeSpan perMessageDelay, string diagnosticContext)
		{
			this.ThrottleDelay = perMessageDelay;
			this.ThrottleDelayContext = diagnosticContext;
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000E0DD1 File Offset: 0x000DEFD1
		// (set) Token: 0x060036D6 RID: 14038 RVA: 0x000E0DD9 File Offset: 0x000DEFD9
		public TimeSpan ThrottleDelay { get; private set; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000E0DE2 File Offset: 0x000DEFE2
		// (set) Token: 0x060036D8 RID: 14040 RVA: 0x000E0DEA File Offset: 0x000DEFEA
		public string ThrottleDelayContext { get; private set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000E0DF4 File Offset: 0x000DEFF4
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				List<ISmtpInSession> list = this.sessions.TakeSnapshot();
				int count = list.Count;
				stringBuilder.Append("SessionCount=");
				stringBuilder.AppendLine(count.ToString(CultureInfo.InvariantCulture));
				for (int i = 0; i < count; i++)
				{
					stringBuilder.Append("Session[");
					stringBuilder.Append(i.ToString(CultureInfo.InvariantCulture));
					stringBuilder.Append("]: ");
					ISmtpInSession smtpInSession = list[i];
					Breadcrumbs<SmtpInSessionBreadcrumbs> breadcrumbs = smtpInSession.Breadcrumbs;
					stringBuilder.Append("LastBcIndex=");
					stringBuilder.Append(breadcrumbs.LastFilledIndex.ToString(CultureInfo.InvariantCulture));
					stringBuilder.Append("; BcData=");
					foreach (SmtpInSessionBreadcrumbs smtpInSessionBreadcrumbs in breadcrumbs.BreadCrumb)
					{
						stringBuilder.Append(smtpInSessionBreadcrumbs);
						stringBuilder.Append(",");
					}
					stringBuilder.Append("; ");
					SmtpSession sessionSource = smtpInSession.SessionSource;
					if (sessionSource != null)
					{
						IExecutionControl executionControl = sessionSource.ExecutionControl;
						if (executionControl == null)
						{
							stringBuilder.Append("MExSession=null");
						}
						else
						{
							stringBuilder.Append("Agent=");
							stringBuilder.Append(executionControl.ExecutingAgentName);
						}
					}
					stringBuilder.AppendLine();
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000E0F54 File Offset: 0x000DF154
		public SmtpOutConnectionHandler SmtpOutConnectionHandler
		{
			get
			{
				return this.smtpOutConnectionHandler;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000E0F5C File Offset: 0x000DF15C
		public SmtpProxyPerfCountersWrapper ClientProxyPerfCounters
		{
			get
			{
				return this.clientProxyPerfCounters;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000E0F64 File Offset: 0x000DF164
		public SmtpProxyPerfCountersWrapper OutboundProxyPerfCounters
		{
			get
			{
				return this.outboundProxyPerfCounters;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000E0F6C File Offset: 0x000DF16C
		public OutboundProxyBySourceTracker OutboundProxyBySourceTracker
		{
			get
			{
				return this.outboundProxyBySourceTracker;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000E0F74 File Offset: 0x000DF174
		// (set) Token: 0x060036DF RID: 14047 RVA: 0x000E0F7C File Offset: 0x000DF17C
		public ISmtpReceiveConfiguration ReceiveConfiguration { get; private set; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000E0F85 File Offset: 0x000DF185
		public IProxyHubSelector ProxyHubSelector
		{
			get
			{
				return this.proxyHubSelector;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000E0F8D File Offset: 0x000DF18D
		public IPConnectionTable InboundTlsIPConnectionTable
		{
			get
			{
				return this.inboundTlsIPConnectionTable;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000E0F95 File Offset: 0x000DF195
		public bool Ipv6ReceiveConnectionThrottlingEnabled
		{
			get
			{
				return this.ipv6ReceiveConnectionThrottlingEnabled;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000E0F9D File Offset: 0x000DF19D
		public bool ReceiveTlsThrottlingEnabled
		{
			get
			{
				return this.receiveTlsThrottlingEnabled;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000E0FA5 File Offset: 0x000DF1A5
		public IEventNotificationItem EventNotificationItem
		{
			get
			{
				return this.eventNotificationItem;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000E0FAD File Offset: 0x000DF1AD
		private bool SelfListening
		{
			get
			{
				return this.tcpListener != null;
			}
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000E0FBC File Offset: 0x000DF1BC
		public void SetRunTimeDependencies(IAgentRuntime agentRuntime, IMailRouter mailRouter, IProxyHubSelector proxyHubSelector, IEnhancedDns enhancedDns, ICategorizer categorizer, ICertificateCache certificateCache, ICertificateValidator certificateValidator, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, ISmtpInMailItemStorage mailItemStorage, SmtpOutConnectionHandler smtpOutConnectionHandler, IQueueQuotaComponent queueQuotaComponent)
		{
			if (enhancedDns == null)
			{
				throw new ArgumentNullException("enhancedDns");
			}
			this.agentRuntime = agentRuntime;
			this.mailRouter = mailRouter;
			this.enhancedDns = enhancedDns;
			this.categorizer = categorizer;
			this.certificateCache = certificateCache;
			this.certificateValidator = certificateValidator;
			this.memberOfResolver = memberOfResolver;
			this.messageThrottlingManager = messageThrottlingManager;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.mailItemStorage = mailItemStorage;
			this.serverVersion = this.ServerConfiguration.AdminDisplayVersion;
			this.queueQuotaComponent = queueQuotaComponent;
			if (this.shadowRedundancyManager != null)
			{
				this.shadowRedundancyManager.SetDelayedAckCompletedHandler(new DelayedAckItemHandler(SmtpInSession.DelayedAckCompletedCallback));
			}
			this.smtpOutConnectionHandler = smtpOutConnectionHandler;
			this.proxyHubSelector = proxyHubSelector;
			this.inboundTlsIPConnectionTable = new IPConnectionTable(this.configuration.LocalServer.TransportServer.MaxReceiveTlsRatePerMinute);
			this.ipv6ReceiveConnectionThrottlingEnabled = this.transportAppConfig.SmtpReceiveConfiguration.Ipv6ReceiveConnectionThrottlingEnabled;
			this.receiveTlsThrottlingEnabled = this.transportAppConfig.SmtpReceiveConfiguration.ReceiveTlsThrottlingEnabled;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000E10C0 File Offset: 0x000DF2C0
		public void SetLoadTimeDependencies(IProtocolLog protocolLog, ITransportAppConfig transportAppConfig, ITransportConfiguration configuration)
		{
			this.ReceiveConfiguration = SmtpReceiveConfiguration.Create(transportAppConfig, configuration);
			this.protocolLogBufferSize = transportAppConfig.Logging.SmtpRecvLogBufferSize;
			this.protocolLogStreamFlushInterval = transportAppConfig.Logging.SmtpRecvLogFlushInterval;
			this.protocolLogAsyncInterval = transportAppConfig.Logging.SmtpRecvLogAsyncInterval;
			this.protocolLog = protocolLog;
			this.transportAppConfig = transportAppConfig;
			this.configuration = configuration;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000E1124 File Offset: 0x000DF324
		public void Load()
		{
			this.CreateBlindProxyPerfCounters();
			this.ConfigureProtocolLog(this.ServerConfiguration);
			this.ReconfigureTransportServer(this.configuration.LocalServer);
			this.ReconfigureConnectors(this.configuration.LocalReceiveConnectors);
			this.configuration.LocalServerChanged += this.ReconfigureTransportServer;
			this.configuration.LocalReceiveConnectorsChanged += this.ReconfigureConnectors;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000E1193 File Offset: 0x000DF393
		public void Unload()
		{
			this.configuration.LocalServerChanged -= this.ReconfigureTransportServer;
			this.configuration.LocalReceiveConnectorsChanged -= this.ReconfigureConnectors;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000E11C3 File Offset: 0x000DF3C3
		public void NonGracefullyCloseTcpListener()
		{
			if (this.tcpListener != null)
			{
				this.tcpListener.StopListening();
			}
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000E11D8 File Offset: 0x000DF3D8
		public void Initialize(TcpListener.HandleFailure failureDelegate = null, TcpListener.HandleConnection connectionHandler = null)
		{
			this.ReconfigureTransportServer(this.configuration.LocalServer);
			if (failureDelegate != null && connectionHandler != null)
			{
				this.tcpListener = new TcpListener(failureDelegate, connectionHandler, null, ExTraceGlobals.SmtpReceiveTracer, this.eventLogger, this.maxConnectionRate, this.transportAppConfig.SmtpReceiveConfiguration.ExclusiveAddressUse, this.transportAppConfig.SmtpReceiveConfiguration.DisableHandleInheritance);
			}
			this.ReconfigureConnectors(this.configuration.LocalReceiveConnectors);
			if (this.SelfListening)
			{
				this.tcpListener.StartListening(true);
			}
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000E1260 File Offset: 0x000DF460
		public void Shutdown()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "Shutdown called");
			while (this.sessionsPendingCreation > 0)
			{
				Thread.Sleep(100);
			}
			if (this.SelfListening)
			{
				this.tcpListener.ProcessStopping = true;
			}
			this.sessions.StartShuttingDown();
			if (this.SelfListening)
			{
				this.tcpListener.StopListening();
				this.tcpListener.Shutdown();
				try
				{
					this.configUpdateLock.EnterWriteLock();
					this.tcpListener = null;
					if (this.bindings != null)
					{
						this.bindings.Clear();
					}
				}
				finally
				{
					try
					{
						this.configUpdateLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			this.sessions.ShutdownAllSessionsAndBlockUntilComplete(this.transportAppConfig.SmtpReceiveConfiguration.WaitForSmtpSessionsAtShutdown);
			this.protocolLog.Close();
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000E134C File Offset: 0x000DF54C
		public INetworkConnection CreateNetworkConnection(Socket socket, int receiveBufferSize)
		{
			ArgumentValidator.ThrowIfNull("socket", socket);
			return new NetworkConnection(socket, receiveBufferSize);
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000E1360 File Offset: 0x000DF560
		public bool HandleConnection(INetworkConnection connection)
		{
			try
			{
				Interlocked.Increment(ref this.sessionsPendingCreation);
				SmtpReceiveConnectorStub smtpReceiveConnectorStub = null;
				connection.MaxLineLength = 4000;
				if (this.connectorMap != null)
				{
					if (connection.LocalEndPoint.Address.Equals(IPAddress.Any) || connection.RemoteEndPoint.Address.Equals(IPAddress.Any))
					{
						return false;
					}
					try
					{
						this.configUpdateLock.EnterReadLock();
						smtpReceiveConnectorStub = this.connectorMap.Lookup(connection.LocalEndPoint.Address, connection.LocalEndPoint.Port, connection.RemoteEndPoint.Address);
					}
					finally
					{
						try
						{
							this.configUpdateLock.ExitReadLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
				if (smtpReceiveConnectorStub == null || smtpReceiveConnectorStub.Connector == null)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "Mapped to connector: not found");
				}
				else
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string>((long)this.GetHashCode(), "Mapped to connector: Id={0}", smtpReceiveConnectorStub.Connector.Name);
				}
				ISmtpInSession smtpInSession = new SmtpInSession(connection, this, smtpReceiveConnectorStub, this.protocolLog, this.eventLogger, this.agentRuntime, this.mailRouter, this.enhancedDns, this.memberOfResolver, this.messageThrottlingManager, this.shadowRedundancyManager, this.transportAppConfig, this.configuration, this.queueQuotaComponent, this.authzAuthorization, this.smtpmessageContextBlob);
				if (this.sessions.TryAdd(connection.ConnectionId, smtpInSession))
				{
					smtpInSession.Start();
				}
				else
				{
					smtpInSession.Shutdown();
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.sessionsPendingCreation);
			}
			return true;
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000E152C File Offset: 0x000DF72C
		public void AddDiagnosticInfo(DiagnosableParameters parameters, XElement element)
		{
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag2)
			{
				element.Add(new XElement("help", "Supported arguments: verbose, help."));
				return;
			}
			if (this.inboundProxyDestinationTracker != null)
			{
				XElement xelement;
				if (this.inboundProxyDestinationTracker.TryGetDiagnosticInfo(parameters, out xelement) && xelement != null)
				{
					element.Add(xelement);
				}
				if (this.inboundProxyAccountForestTracker.TryGetDiagnosticInfo(parameters, out xelement) && xelement != null)
				{
					element.Add(xelement);
				}
			}
			List<ISmtpInSession> list = this.sessions.TakeSnapshot();
			if (flag)
			{
				XElement xelement2 = new XElement("Sessions");
				element.Add(xelement2);
				using (List<ISmtpInSession>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ISmtpInSession smtpInSession = enumerator.Current;
						XElement xelement3 = new XElement("Session");
						xelement2.Add(xelement3);
						xelement3.SetAttributeValue("LocalEndPoint", smtpInSession.NetworkConnection.LocalEndPoint);
						xelement3.SetAttributeValue("RemoteEndPoint", smtpInSession.NetworkConnection.RemoteEndPoint);
						xelement3.SetAttributeValue("HelloSmtpDomain", smtpInSession.HelloSmtpDomain ?? "null");
						xelement3.SetAttributeValue("SessionStartTime", smtpInSession.SessionStartTime);
						xelement3.SetAttributeValue("SessionId", smtpInSession.SessionId.ToString("X"));
						xelement3.SetAttributeValue("RemoteIdentityName", smtpInSession.RemoteIdentityName ?? "null");
						xelement3.SetAttributeValue("Breadcrumbs", smtpInSession.Breadcrumbs.ToString().Replace("\r\n", ";"));
					}
					return;
				}
			}
			XElement xelement4 = new XElement("Sessions");
			element.Add(xelement4);
			xelement4.SetAttributeValue("NumSessions", list.Count);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000E1778 File Offset: 0x000DF978
		public void RemoveConnection(long id)
		{
			this.sessions.Remove(id);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000E1786 File Offset: 0x000DF986
		private static SmtpProxyPerfCountersWrapper GetSmtpProxyPerfCounterInstance(string connectorName)
		{
			return new SmtpProxyPerfCountersWrapper(connectorName);
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000E178E File Offset: 0x000DF98E
		private static OutboundProxyBySourceTracker GetPerResourceForestOutboundProxyTrackerInstance()
		{
			return new OutboundProxyBySourceTracker(Components.TransportAppConfig.SmtpOutboundProxyConfiguration.ResourceForestMatchingDomains);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000E17A4 File Offset: 0x000DF9A4
		private void CreateBlindProxyPerfCounters()
		{
			if (this.Configuration.ProcessTransportRole == ProcessTransportRole.FrontEnd)
			{
				this.clientProxyPerfCounters = SmtpInServer.GetSmtpProxyPerfCounterInstance("Client submission proxy");
				this.outboundProxyPerfCounters = SmtpInServer.GetSmtpProxyPerfCounterInstance("Outbound proxy");
				this.outboundProxyBySourceTracker = SmtpInServer.GetPerResourceForestOutboundProxyTrackerInstance();
			}
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000E17DF File Offset: 0x000DF9DF
		private void ConfigureProtocolLog(Server serverConfig)
		{
			this.protocolLog.Configure(serverConfig.ReceiveProtocolLogPath, serverConfig.ReceiveProtocolLogMaxAge, serverConfig.ReceiveProtocolLogMaxDirectorySize, serverConfig.ReceiveProtocolLogMaxFileSize, this.protocolLogBufferSize, this.protocolLogStreamFlushInterval, this.protocolLogAsyncInterval);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000E181C File Offset: 0x000DFA1C
		private void TcpListenerSetBindings()
		{
			IPEndPoint[] newBindings;
			try
			{
				this.configUpdateLock.EnterReadLock();
				newBindings = ((this.bindings != null) ? this.bindings.ToArray() : null);
			}
			finally
			{
				try
				{
					this.configUpdateLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			this.tcpListener.SetBindings(newBindings, false);
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000E1888 File Offset: 0x000DFA88
		private void ReconfigureTransportServer(TransportServerConfiguration config)
		{
			this.ConfigureProtocolLog(config.TransportServer);
			this.maxConnectionRate = config.TransportServer.MaxConnectionRatePerMinute;
			if (this.SelfListening)
			{
				this.tcpListener.MaxConnectionRate = this.maxConnectionRate;
			}
			if (this.mailboxDeliveryReceiveConnector != null)
			{
				this.mailboxDeliveryReceiveConnector.ApplyLocalServerConfiguration(config.TransportServer);
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000E18E4 File Offset: 0x000DFAE4
		private void ReconfigureConnectors(ReceiveConnectorConfiguration connectorConfig)
		{
			ServerRole serverRole;
			switch (this.configuration.ProcessTransportRole)
			{
			case ProcessTransportRole.Hub:
				serverRole = ServerRole.HubTransport;
				break;
			case ProcessTransportRole.Edge:
				serverRole = ServerRole.HubTransport;
				break;
			case ProcessTransportRole.FrontEnd:
				serverRole = ServerRole.FrontendTransport;
				break;
			case ProcessTransportRole.MailboxSubmission:
			case ProcessTransportRole.MailboxDelivery:
				serverRole = ServerRole.Mailbox;
				break;
			default:
				throw new InvalidOperationException("invalid value for ProcessTransportRole");
			}
			List<ReceiveConnector> list = new List<ReceiveConnector>();
			foreach (ReceiveConnector receiveConnector in connectorConfig.Connectors)
			{
				if ((receiveConnector.TransportRole & serverRole) > ServerRole.None)
				{
					list.Add(receiveConnector);
				}
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "Number of SMTP Receive Connectors that apply to this process ({0}) = {1}, out of a total of {2}", this.configuration.ProcessTransportRole.ToString(), list.Count, connectorConfig.Connectors.Count);
			if (this.configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery)
			{
				if (this.mailboxDeliveryReceiveConnector == null)
				{
					this.mailboxDeliveryReceiveConnector = new MailboxDeliveryReceiveConnector(string.Format(CultureInfo.InvariantCulture, "Default Mailbox Delivery {0}", new object[]
					{
						Environment.MachineName
					}), this.configuration.LocalServer.TransportServer, this.transportAppConfig.SmtpReceiveConfiguration.MailboxDeliveryAcceptAnonymousUsers);
				}
				list.Add(this.mailboxDeliveryReceiveConnector);
			}
			if (list.Count == 0)
			{
				try
				{
					this.configUpdateLock.EnterWriteLock();
					this.connectorMap = null;
					this.connectorStubList = null;
					this.bindings = null;
				}
				finally
				{
					try
					{
						this.configUpdateLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "The list of connectors is empty. All bindings are closed.");
			}
			else
			{
				SmtpInConnectorMap<SmtpReceiveConnectorStub> newConnectorMap;
				List<SmtpReceiveConnectorStub> newConnectorStubList;
				List<IPEndPoint> newBindings;
				this.ProcessReceiveConnectors(list, out newConnectorMap, out newConnectorStubList, out newBindings);
				this.ConfigureInboundProxyDestinationTrackers(list);
				this.UpdateReceiveConnectors(newConnectorMap, newConnectorStubList, newBindings);
			}
			if (this.SelfListening)
			{
				this.TcpListenerSetBindings();
			}
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000E1B0C File Offset: 0x000DFD0C
		private void ConfigureInboundProxyDestinationTrackers(IEnumerable<ReceiveConnector> receiveConnectors)
		{
			ArgumentValidator.ThrowIfNull("receiveConnectors", receiveConnectors);
			TransportAppConfig.SmtpInboundProxyConfig smtpInboundProxyConfiguration = this.transportAppConfig.SmtpInboundProxyConfiguration;
			if (this.inboundProxyDestinationTracker == null)
			{
				this.inboundProxyDestinationTracker = new InboundProxyDestinationTracker(InboundProxyTrackerType.InboundProxyDestinationTracker, smtpInboundProxyConfiguration.InboundProxyDestinationTrackingEnabled, smtpInboundProxyConfiguration.RejectBasedOnInboundProxyDestinationTrackingEnabled, smtpInboundProxyConfiguration.PerDestinationConnectionPercentageThreshold, new InboundProxyDestinationTracker.TryGetDestinationConnectionThresholdDelegate(smtpInboundProxyConfiguration.TryGetDestinationConnectionThreshold), (string instanceName) => InboundProxyDestinationPerfCounters.GetInstance(instanceName).ConnectionsCurrent, (string instanceName) => InboundProxyDestinationPerfCounters.GetInstance(instanceName).ConnectionsTotal, new ExEventLogWrapper(this.eventLogger), smtpInboundProxyConfiguration.InboundProxyDestinationTrackerLogInterval, receiveConnectors);
			}
			else
			{
				this.inboundProxyDestinationTracker.UpdateReceiveConnectors(receiveConnectors);
			}
			if (this.inboundProxyAccountForestTracker == null)
			{
				this.inboundProxyAccountForestTracker = new InboundProxyDestinationTracker(InboundProxyTrackerType.InboundProxyAccountForestTracker, smtpInboundProxyConfiguration.InboundProxyAccountForestTrackingEnabled, smtpInboundProxyConfiguration.RejectBasedOnInboundProxyAccountForestTrackingEnabled, smtpInboundProxyConfiguration.PerAccountForestConnectionPercentageThreshold, new InboundProxyDestinationTracker.TryGetDestinationConnectionThresholdDelegate(smtpInboundProxyConfiguration.TryGetAccountForestConnectionThreshold), (string instanceName) => InboundProxyAccountForestPerfCounters.GetInstance(instanceName).ConnectionsCurrent, (string instanceName) => InboundProxyAccountForestPerfCounters.GetInstance(instanceName).ConnectionsTotal, new ExEventLogWrapper(this.eventLogger), smtpInboundProxyConfiguration.InboundProxyDestinationTrackerLogInterval, receiveConnectors);
				return;
			}
			this.inboundProxyAccountForestTracker.UpdateReceiveConnectors(receiveConnectors);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000E1C48 File Offset: 0x000DFE48
		private void UpdateReceiveConnectors(SmtpInConnectorMap<SmtpReceiveConnectorStub> newConnectorMap, List<SmtpReceiveConnectorStub> newConnectorStubList, List<IPEndPoint> newBindings)
		{
			try
			{
				this.configUpdateLock.EnterWriteLock();
				List<SmtpReceiveConnectorStub> list = this.connectorStubList;
				this.connectorMap = newConnectorMap;
				this.connectorStubList = newConnectorStubList;
				this.bindings = newBindings;
				if (list != null)
				{
					foreach (SmtpReceiveConnectorStub smtpReceiveConnectorStub in list)
					{
						foreach (SmtpReceiveConnectorStub smtpReceiveConnectorStub2 in this.connectorStubList)
						{
							if (smtpReceiveConnectorStub.Connector.Identity.Equals(smtpReceiveConnectorStub2.Connector.Identity))
							{
								smtpReceiveConnectorStub2.ConnectionTable = smtpReceiveConnectorStub.ConnectionTable;
								break;
							}
						}
					}
				}
			}
			finally
			{
				try
				{
					this.configUpdateLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ConfiguredConnectors, null, null);
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000E1D64 File Offset: 0x000DFF64
		private void ProcessReceiveConnectors(List<ReceiveConnector> connectors, out SmtpInConnectorMap<SmtpReceiveConnectorStub> newConnectorMap, out List<SmtpReceiveConnectorStub> newConnectorStubList, out List<IPEndPoint> newBindings)
		{
			newConnectorMap = new SmtpInConnectorMap<SmtpReceiveConnectorStub>();
			newConnectorStubList = new List<SmtpReceiveConnectorStub>();
			newBindings = new List<IPEndPoint>();
			foreach (ReceiveConnector receiveConnector in connectors)
			{
				if (receiveConnector.Enabled)
				{
					foreach (IPBinding ipbinding in receiveConnector.Bindings)
					{
						newBindings.Add(new IPEndPoint(ipbinding.Address, ipbinding.Port));
					}
					SmtpReceiveConnectorStub smtpReceiveConnectorStub = new SmtpReceiveConnectorStub(receiveConnector, Util.CreateReceivePerfCounters(receiveConnector, this.configuration.ProcessTransportRole), Util.GetOrCreateAvailabilityPerfCounters(this.cachedAvailabilityPerfCounters, receiveConnector, this.configuration.ProcessTransportRole, this.transportAppConfig.SmtpAvailabilityConfiguration.SmtpAvailabilityMinConnectionsToMonitor));
					newConnectorMap.AddEntry(receiveConnector.Bindings.ToArray(), receiveConnector.RemoteIPRanges.ToArray(), smtpReceiveConnectorStub);
					newConnectorStubList.Add(smtpReceiveConnectorStub);
				}
			}
		}

		// Token: 0x04001BE8 RID: 7144
		private ServiceState targetRunningState;

		// Token: 0x04001BE9 RID: 7145
		private IPConnectionTable inboundTlsIPConnectionTable;

		// Token: 0x04001BEA RID: 7146
		private bool ipv6ReceiveConnectionThrottlingEnabled;

		// Token: 0x04001BEB RID: 7147
		private bool receiveTlsThrottlingEnabled;

		// Token: 0x04001BEC RID: 7148
		private MailboxDeliveryReceiveConnector mailboxDeliveryReceiveConnector;

		// Token: 0x04001BED RID: 7149
		private readonly ExEventLog eventLogger;

		// Token: 0x04001BEE RID: 7150
		private IInboundProxyDestinationTracker inboundProxyDestinationTracker;

		// Token: 0x04001BEF RID: 7151
		private IInboundProxyDestinationTracker inboundProxyAccountForestTracker;

		// Token: 0x04001BF0 RID: 7152
		private ICategorizer categorizer;

		// Token: 0x04001BF1 RID: 7153
		private ISmtpInMailItemStorage mailItemStorage;

		// Token: 0x04001BF2 RID: 7154
		private IProtocolLog protocolLog;

		// Token: 0x04001BF3 RID: 7155
		private IMailRouter mailRouter;

		// Token: 0x04001BF4 RID: 7156
		private IEnhancedDns enhancedDns;

		// Token: 0x04001BF5 RID: 7157
		private ICertificateCache certificateCache;

		// Token: 0x04001BF6 RID: 7158
		private ICertificateValidator certificateValidator;

		// Token: 0x04001BF7 RID: 7159
		private IShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001BF8 RID: 7160
		private IMessageThrottlingManager messageThrottlingManager;

		// Token: 0x04001BF9 RID: 7161
		private IIsMemberOfResolver<RoutingAddress> memberOfResolver;

		// Token: 0x04001BFA RID: 7162
		private ITransportAppConfig transportAppConfig;

		// Token: 0x04001BFB RID: 7163
		private ITransportConfiguration configuration;

		// Token: 0x04001BFC RID: 7164
		private IQueueQuotaComponent queueQuotaComponent;

		// Token: 0x04001BFD RID: 7165
		private IAgentRuntime agentRuntime;

		// Token: 0x04001BFE RID: 7166
		private DateTime currentTime;

		// Token: 0x04001BFF RID: 7167
		private Version serverVersion;

		// Token: 0x04001C00 RID: 7168
		private readonly SmtpSessions sessions = new SmtpSessions();

		// Token: 0x04001C01 RID: 7169
		private SmtpInConnectorMap<SmtpReceiveConnectorStub> connectorMap;

		// Token: 0x04001C02 RID: 7170
		private List<SmtpReceiveConnectorStub> connectorStubList;

		// Token: 0x04001C03 RID: 7171
		private readonly ReaderWriterLockSlim configUpdateLock;

		// Token: 0x04001C04 RID: 7172
		private TcpListener tcpListener;

		// Token: 0x04001C05 RID: 7173
		private List<IPEndPoint> bindings = new List<IPEndPoint>();

		// Token: 0x04001C06 RID: 7174
		private int maxConnectionRate = 1200;

		// Token: 0x04001C07 RID: 7175
		private int sessionsPendingCreation;

		// Token: 0x04001C08 RID: 7176
		private readonly ConcurrentDictionary<string, ISmtpAvailabilityPerfCounters> cachedAvailabilityPerfCounters = new ConcurrentDictionary<string, ISmtpAvailabilityPerfCounters>();

		// Token: 0x04001C09 RID: 7177
		private int protocolLogBufferSize;

		// Token: 0x04001C0A RID: 7178
		private TimeSpan protocolLogStreamFlushInterval;

		// Token: 0x04001C0B RID: 7179
		private TimeSpan protocolLogAsyncInterval;

		// Token: 0x04001C0C RID: 7180
		private SmtpOutConnectionHandler smtpOutConnectionHandler;

		// Token: 0x04001C0D RID: 7181
		private SmtpProxyPerfCountersWrapper clientProxyPerfCounters;

		// Token: 0x04001C0E RID: 7182
		private SmtpProxyPerfCountersWrapper outboundProxyPerfCounters;

		// Token: 0x04001C0F RID: 7183
		private OutboundProxyBySourceTracker outboundProxyBySourceTracker;

		// Token: 0x04001C10 RID: 7184
		private IProxyHubSelector proxyHubSelector;

		// Token: 0x04001C11 RID: 7185
		private readonly IAuthzAuthorization authzAuthorization = new AuthzAuthorizationWrapper();

		// Token: 0x04001C12 RID: 7186
		private readonly ISmtpMessageContextBlob smtpmessageContextBlob = new SmtpMessageContextBlobWrapper();

		// Token: 0x04001C13 RID: 7187
		private readonly IEventNotificationItem eventNotificationItem = new EventNotificationItemWrapper();
	}
}
